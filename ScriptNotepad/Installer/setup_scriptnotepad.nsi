# MIT License
# 
# Copyright(c) 2019 Petteri Kautonen
# 
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
# 
# The above copyright notice and this permission notice shall be included in all
# copies or substantial portions of the Software.
# 
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
# SOFTWARE.


SetCompressor /SOLID /FINAL lzma

Name "ScriptNotepad"

# General Symbol Definitions
!define REGKEY "SOFTWARE\$(^Name)"
!define VERSION 1.0.0.0
!define COMPANY VPKSoft
!define URL http://www.vpksoft.net

# MUI Symbol Definitions
!define MUI_ICON ..\notepad7.ico
!define MUI_FINISHPAGE_NOAUTOCLOSE
!define MUI_STARTMENUPAGE_REGISTRY_ROOT HKLM
!define MUI_STARTMENUPAGE_NODISABLE
!define MUI_STARTMENUPAGE_REGISTRY_KEY ${REGKEY}
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME StartMenuGroup
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "ScriptNotepad"
!define MUI_UNICON .\un_icon.ico
!define MUI_UNFINISHPAGE_NOAUTOCLOSE
!define MUI_LANGDLL_REGISTRY_ROOT HKLM
!define MUI_LANGDLL_REGISTRY_KEY ${REGKEY}
!define MUI_LANGDLL_REGISTRY_VALUENAME InstallerLanguage
BrandingText "ScriptNotepad"

!include 'LogicLib.nsh'
!include "x64.nsh"
!include "InstallOptions.nsh"
!include "DotNetChecker.nsh"

# Included files
!include Sections.nsh
!include MUI2.nsh

# Reserved Files
!insertmacro MUI_RESERVEFILE_LANGDLL

# Variables
Var StartMenuGroup

# Installer pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE .\license.txt
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_STARTMENU Application $StartMenuGroup
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

# Installer languages
!insertmacro MUI_LANGUAGE English
!insertmacro MUI_LANGUAGE Finnish

# Installer attributes
OutFile setup_scriptnotepad_1_0_0_0.exe
InstallDir "$PROGRAMFILES64\ScriptNotepad"
CRCCheck on
XPStyle on
ShowInstDetails hide
VIProductVersion 1.0.0.0
VIAddVersionKey /LANG=${LANG_ENGLISH} ProductName "ScriptNotepad installer"
VIAddVersionKey /LANG=${LANG_ENGLISH} ProductVersion "${VERSION}"
VIAddVersionKey /LANG=${LANG_ENGLISH} CompanyName "${COMPANY}"
VIAddVersionKey /LANG=${LANG_ENGLISH} CompanyWebsite "${URL}"
VIAddVersionKey /LANG=${LANG_ENGLISH} FileVersion "${VERSION}"
VIAddVersionKey /LANG=${LANG_ENGLISH} FileDescription "ScriptNotepad"
VIAddVersionKey /LANG=${LANG_ENGLISH} LegalCopyright "Copyright � VPKSoft 2019"
InstallDirRegKey HKLM "${REGKEY}" Path
ShowUninstDetails hide

# Installer sections
Section -Main SEC0000
    SetOutPath $INSTDIR
    SetOverwrite on

    !insertmacro CheckNetFramework 47	
   
    SetOutPath $INSTDIR

    File /r ..\bin\Release\*.*
	File .\languages.ico
		
    SetOutPath "$LOCALAPPDATA\ScriptNotepad"
    File ..\Localization\SQLiteDatabase\lang.sqlite   
	
	#English dictionaries..
    SetOutPath "$LOCALAPPDATA\ScriptNotepad\Dictionaries\en"

	#replace this folder in order to compile the installer successfully..
    File C:\Files\GitHub\dictionaries\en\en_GB.dic  
    File C:\Files\GitHub\dictionaries\en\en_GB.aff  

    File C:\Files\GitHub\dictionaries\en\en_US.dic  
    File C:\Files\GitHub\dictionaries\en\en_US.aff  

	#replace this folder in order to compile the installer successfully..
	SetOutPath "$LOCALAPPDATA\ScriptNotepad\Notepad-plus-plus-themes"
	File /r C:\Files\GitHub\notepad-plus-plus\PowerEditor\installer\themes
   
	SetOutPath $SMPROGRAMS\$StartMenuGroup
    CreateShortcut "$SMPROGRAMS\$StartMenuGroup\ScriptNotepad.lnk" $INSTDIR\ScriptNotepad.exe	
	CreateShortcut "$SMPROGRAMS\$StartMenuGroup\$(LocalizeDesc).lnk" "$INSTDIR\ScriptNotepad.exe" '"--localize=$LOCALAPPDATA\ScriptNotepad\lang.sqlite" ' "$INSTDIR\languages.ico" 0
	
    WriteRegStr HKLM "${REGKEY}\Components" Main 1
	
    MessageBox MB_YESNO|MB_ICONQUESTION|MB_DEFBUTTON1 "$(AddMenuToShell)" IDYES addMenu IDNO noAddMenu

	addMenu:
    DetailPrint "User chose to associate with the Windows shell..."
	Call MakeShellMenu
    noAddMenu:
    DetailPrint "User chose NOT to associate with the Windows shell..."
SectionEnd

Section -post SEC0001
    WriteRegStr HKLM "${REGKEY}" Path $INSTDIR
    SetOutPath $INSTDIR
    WriteUninstaller $INSTDIR\uninstall_scriptnotepad.exe
    !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    SetOutPath $SMPROGRAMS\$StartMenuGroup
    CreateShortcut "$SMPROGRAMS\$StartMenuGroup\$(^UninstallLink).lnk" $INSTDIR\uninstall_scriptnotepad.exe
    !insertmacro MUI_STARTMENU_WRITE_END
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayName "$(^Name)"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayVersion "${VERSION}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" Publisher "${COMPANY}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" URLInfoAbout "${URL}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayIcon $INSTDIR\uninstall_scriptnotepad.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" UninstallString $INSTDIR\uninstall_scriptnotepad.exe
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoModify 1
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoRepair 1
SectionEnd

; !defines for use with SHChangeNotify
!ifdef SHCNE_ASSOCCHANGED
!undef SHCNE_ASSOCCHANGED
!endif
!define SHCNE_ASSOCCHANGED 0x08000000
!ifdef SHCNF_FLUSH
!undef SHCNF_FLUSH
!endif
!define SHCNF_FLUSH        0x1000

# Macro for selecting uninstaller sections
!macro SELECT_UNSECTION SECTION_NAME UNSECTION_ID
    Push $R0
    ReadRegStr $R0 HKLM "${REGKEY}\Components" "${SECTION_NAME}"
    StrCmp $R0 1 0 next${UNSECTION_ID}
    !insertmacro SelectSection "${UNSECTION_ID}"
    GoTo done${UNSECTION_ID}
next${UNSECTION_ID}:
    !insertmacro UnselectSection "${UNSECTION_ID}"
done${UNSECTION_ID}:
    Pop $R0
!macroend

# Uninstaller sections
Section /o -un.Main UNSEC0000
    Delete /REBOOTOK "$SMPROGRAMS\$StartMenuGroup\ScriptNotepad.lnk"
	Delete /REBOOTOK "$SMPROGRAMS\$StartMenuGroup\$(LocalizeDesc).lnk"

	RMDir /r /REBOOTOK $INSTDIR
	
	Call un.DeleteLocalData
	
	Call un.MakeShellMenu
	
    DeleteRegValue HKLM "${REGKEY}\Components" Main
SectionEnd

Section -un.post UNSEC0001
    DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)"
    Delete /REBOOTOK "$SMPROGRAMS\$StartMenuGroup\$(^UninstallLink).lnk"
    Delete /REBOOTOK $INSTDIR\uninstall_scriptnotepad.exe
    DeleteRegValue HKLM "${REGKEY}" StartMenuGroup
    DeleteRegValue HKLM "${REGKEY}" Path
    DeleteRegKey /IfEmpty HKLM "${REGKEY}\Components"
    DeleteRegKey /IfEmpty HKLM "${REGKEY}"	
	
    RMDir /REBOOTOK $SMPROGRAMS\$StartMenuGroup
	
    RMDir /REBOOTOK $INSTDIR
SectionEnd

# Installer functions
Function .onInit
    InitPluginsDir
    !insertmacro MUI_LANGDLL_DISPLAY    
FunctionEnd

# Uninstaller functions
Function un.onInit
    ReadRegStr $INSTDIR HKLM "${REGKEY}" Path
    !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuGroup
    !insertmacro MUI_UNGETLANGUAGE
    !insertmacro SELECT_UNSECTION Main ${UNSEC0000}
FunctionEnd

Function un.DeleteLocalData
	MessageBox MB_ICONQUESTION|MB_YESNO "$(DeleteUserData)" IDYES deletelocal IDNO nodeletelocal
	deletelocal:
    RMDir /r /REBOOTOK "$LOCALAPPDATA\ScriptNotepad"
	nodeletelocal:
FunctionEnd

Function MakeShellMenu
	WriteRegStr HKCR "*\shell\$(OpenWithScriptNotepad)" "Icon" "$\"$INSTDIR\ScriptNotepad.exe$\",0"
	WriteRegStr HKCR "*\shell\$(OpenWithScriptNotepad)\command" "" "$\"$INSTDIR\ScriptNotepad.exe$\"  $\"%1$\""
	System::Call "shell32::SHChangeNotify(i,i,i,i) (${SHCNE_ASSOCCHANGED}, ${SHCNF_FLUSH}, 0, 0)"
FunctionEnd

Function un.MakeShellMenu
	DeleteRegKey HKCR "*\shell\$(OpenWithScriptNotepad)"
	System::Call "shell32::SHChangeNotify(i,i,i,i) (${SHCNE_ASSOCCHANGED}, ${SHCNF_FLUSH}, 0, 0)"
FunctionEnd


# Installer Language Strings
# TODO Update the Language Strings with the appropriate translations.

LangString ^UninstallLink ${LANG_ENGLISH} "Uninstall $(^Name)"
LangString ^UninstallLink ${LANG_FINNISH} "Poista $(^Name)"

# localization..
LangString DeleteUserData ${LANG_FINNISH} "Poista paikalliset käyttäjätiedot?"
LangString DeleteUserData ${LANG_ENGLISH} "Delete local user data?"

LangString AddMenuToShell ${LANG_FINNISH} "Lisää avaa sovelluksella ScriptNotepad Windowsin tiedostoselaimen kontekstivalikkoon?"
LangString AddMenuToShell ${LANG_ENGLISH} "Add Open with ScriptNotepad to Windows shell context menu?"

LangString OpenWithScriptNotepad ${LANG_FINNISH} "Avaa sovelluksella ScriptNotepad"
LangString OpenWithScriptNotepad ${LANG_ENGLISH} "Open with ScriptNotepad"

LangString LocalizeDesc ${LANG_FINNISH} "Lokalisointi"
LangString LocalizeDesc ${LANG_ENGLISH} "Localization"
#END: localization..
