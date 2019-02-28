#region License
/*
MIT License

Copyright(c) 2019 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged; // (C): https://github.com/Fody/PropertyChanged, MIT license
using System.Reflection;
using VPKSoft.ConfLib;
using VPKSoft.ErrorLogger;

namespace ScriptNotepad.Settings
{
    /// <summary>
    /// An attribute class for describing a setting name and it's type (VPKSoft.ConfLib).
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)] // target a property only..
    public class SettingAttribute: Attribute
    {
        /// <summary>
        /// Gets or sets the name of the setting (VPKSoft.ConfLib).
        /// </summary>
        public string SettingName { get; set; }

        /// <summary>
        /// Gets or sets the type of the setting (VPKSoft.ConfLib).
        /// </summary>
        public Type SettingType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingAttribute"/> class.
        /// </summary>
        /// <param name="settingName">Name of the setting (VPKSoft.ConfLib).</param>
        /// <param name="type">The type of the setting (VPKSoft.ConfLib).</param>
        public SettingAttribute(string settingName, Type type): base()
        {
            SettingName = settingName; // save the given values..
            SettingType = type;
        }
    }

    /// <summary>
    /// Settings for the ScriptNotepad software.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class Settings : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            if (Conflib == null) // don't initialize if already initialized..
            {
                Conflib = new Conflib
                {
                    AutoCreateSettings = true // set it to auto-create SQLite database tables..
                }; // create a new instance of the Conflib class..
            }

            #region SpecialSetting
            PropertyInfo propertyInfo = // first get the property info for the property..
                GetType().GetProperty("DefaultEncoding", BindingFlags.Instance | BindingFlags.Public);

            // get the setting attribute value of the property.. 
            SettingAttribute settingAttribute = (SettingAttribute)propertyInfo.GetCustomAttribute(typeof(SettingAttribute));

            // set the default encoding value..
            DefaultEncoding = Encoding.GetEncoding(Conflib[settingAttribute.SettingName, DefaultEncoding.WebName]);
            #endregion

            // get all public instance properties of this class..
            PropertyInfo[] propertyInfos = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            // loop through the properties..
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                // a special property to which the Convert class can't be used..
                if (propertyInfos[i].Name == "DefaultEncoding")
                {
                    continue; // ..so do continue..
                }

                try // avoid crashes..
                {
                    // get the SettingAttribute class instance of the property..
                    settingAttribute = (SettingAttribute)propertyInfos[i].GetCustomAttribute(typeof(SettingAttribute));

                    // get the default value for the property..
                    object currentValue = propertyInfos[i].GetValue(this);

                    // set the value for the property using the default value as a
                    // fall-back value..
                    propertyInfos[i].SetValue(this, Convert.ChangeType(Conflib[settingAttribute.SettingName, currentValue.ToString()], settingAttribute.SettingType));
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionLogger.LogError(ex);
                }
            }

            // subscribe the event handler..
            PropertyChanged += Settings_PropertyChanged;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Settings class instance.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // NOTE:: Do use this attribute, if no notification is required from a property: [DoNotNotify]

            try // just try from the beginning..
            {
                PropertyInfo propertyInfo = // first get the property info for the property..
                GetType().GetProperty(e.PropertyName, BindingFlags.Instance | BindingFlags.Public);

                // get the property value..
                object value = propertyInfo?.GetValue(this);

                // get the setting attribute value of the property.. 
                SettingAttribute settingAttribute = (SettingAttribute)propertyInfo.GetCustomAttribute(typeof(SettingAttribute));

                if (value != null && settingAttribute != null)
                {
                    // this is a special case, otherwise try just to use simple types..
                    if (settingAttribute.SettingType == typeof(Encoding))
                    {
                        Encoding encoding = (Encoding)value;
                        Conflib[settingAttribute.SettingName] = encoding.WebName;
                    }
                    else // a simple type..
                    {
                        Conflib[settingAttribute.SettingName] = value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);
            }
        }

        /// <summary>
        /// An instance to a Conflib class.
        /// </summary>
        private Conflib Conflib = null;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
#pragma warning disable CS0067 // disable the CS0067 as the PropertyChanged event is raised via the PropertyChanged.Fody class library..
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067

        #region Settings
        // NOTE::
        // These properties must have a default value for them to work properly with the class logic!

        /// <summary>
        /// Gets or sets the default encoding to be used with the files within this software.
        /// </summary>
        [Setting("main/encoding", typeof(Encoding))]
        public Encoding DefaultEncoding { get; set; } = Encoding.Default;

        /// <summary>
        /// The amount of files to be saved to a document history.
        /// </summary>
        [Setting("gui/history", typeof(int))]
        public int HistoryListAmount { get; set; } = 20;

        /// <summary>
        /// Gets or sets a value indicating whether save closed file contents to database as history.
        /// </summary>
        [Setting("database/historyContents", typeof(bool))]
        public bool SaveFileHistoryContents { get; set; } = true;

        /// <summary>
        /// Gets or sets the save file history contents count.
        /// </summary>
        [Setting("database/historyContentsCount", typeof(int))]
        public int SaveFileHistoryContentsCount { get; set; } = 20;

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // unsubscribe the event handler..
            PropertyChanged -= Settings_PropertyChanged;

            // close the conflib class instance..
            Conflib?.Close();
        }
    }
}
