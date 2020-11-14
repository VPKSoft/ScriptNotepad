<#
MIT License

Copyright(c) 2020 Petteri Kautonen

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
#>

$files = Get-ChildItem -Recurse ..\..\* -Include "*.cs", "*.wxl", "*.txt" -Exclude "*.Designer.cs"

# English (United States);Finnish (Finland)
$languages = "0x00000102;0x00000118"; # see the Files\Localization\SupportedLanguages.cs file..

$locale_running_ids = $languages -split ";"

# replace the #APPLICATION# tag in the installer project with specified application name
foreach ($file in $files)
{
    Write-Output (-join("Change application name for the file: ", $file.FullName, "...")) 
    ((Get-Content -path $file.FullName -Raw) -replace '#APPLICATION#','ScriptNotepad') | Set-Content -Path $file.FullName
    Write-Output (-join("File handled: ", $file.FullName, ".")) 
}

# Enabled the specified languages in the .\Files\Localization\SupportedLanguages.cs files..
$language_enumfile_contents = (Get-Content -path ..\..\Files\Localization\SupportedLanguages.cs -Raw)

# loop through the localization entries specified by the running id number..
foreach ($locale_running_id in $locale_running_ids) 
{    
    # set the specified language as enabled..
    $language_enumfile_contents = $language_enumfile_contents.Replace(-join($locale_running_id, ")]"), -join ($locale_running_id, ", true", ")]"))
}

# update the .\Files\Localization\SupportedLanguages.cs file contents..
Set-Content -path ..\..\Files\Localization\SupportedLanguages.cs -Value $language_enumfile_contents