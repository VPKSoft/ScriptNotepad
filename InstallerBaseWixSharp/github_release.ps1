<#
MIT License

Copyright (c) 2020 Petteri Kautonen

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

Write-Output "Init GitHub release..."

$output_file = "ScriptNotepad\CryptEnvVar.exe"

$download_url = "https://www.vpksoft.net/toolset/CryptEnvVar.exe"

Write-Output "Download file:  $download_url ..."
# No need to remove this: Remove-Item $output_file
(New-Object System.Net.WebClient).DownloadFile($download_url, $output_file)
Write-Output "Download done."

$output_file_signtool = "ScriptNotepad\signtool.exe"
$download_url = "https://www.vpksoft.net/toolset/signtool.exe"

Write-Output "Download file:  $output_file_signtool ..."
# No need to remove this: Remove-Item $output_file
(New-Object System.Net.WebClient).DownloadFile($download_url, $output_file_signtool)
Write-Output "Download done."

# application parameters..
$application = "ScriptNotepad"
$environment_cryptor = "CryptEnvVar.exe"

# create the digital signature..
$arguments = @("-s", $Env:SECRET_KEY, "-e", "CERT_1;CERT_2;CERT_3;CERT_4;CERT_5;CERT_6;CERT_7;CERT_8", "-f", "C:\vpksoft.pfx", "-w", "80", "-i", "-v")
& (-join($application, "\", $environment_cryptor)) $arguments

# register the certificate to the CI image..
$certpw=ConvertTo-SecureString $Env:PFX_PASS –asplaintext –force 
Import-PfxCertificate -FilePath "C:\vpksoft.pfx" -CertStoreLocation Cert:\LocalMachine\My -Password $certpw | Out-Null

$gitreleasemanager = "gitreleasemanager.exe"

# sign and release tags..
if (![string]::IsNullOrEmpty($Env:CIRCLE_TAG)) # only release for tags..
{
    $files = Get-ChildItem $Env:CIRCLE_WORKING_DIRECTORY -r -Filter *ScriptNotepad*.msi # use the mask to discard possible third party packages..
    for ($i = 0; $i -lt $files.Count; $i++) 
    { 
        $file = $files[$i].FullName

        # sign the MSI installer packages (SHA1).
	    Write-Output (-join("Signing installer package (MSI, SHA1): ", $file, " ..."))

        $arguments = @("sign", "/f", "C:\vpksoft.pfx", "/p", $Env:PFX_PASS, "/sha1", $Env:CERT_HASH, "/t", "http://timestamp.comodoca.com/authenticode", $file)

	    # on the second time, something about 'Keyset does not exist'. TODO::Clean temp?
        & $output_file_signtool $arguments #> null 2>&1
	    Write-Output (-join("Installer package (MSI) signed: ", $file, "."))

        # After signing, clean up the temporary folder, if this helps with the multiple package signing..
        Remove-Item -Recurse -Force (-join($Env:LocalAppData, "\Temp\*.*"))

	    Write-Output (-join("Publishing release: ", $file, " ..."))
        # Useless, as the tag contains the version: $version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($release_exe)
        # Useless, as the tag contains the version: $versionString = (-join("v.", $version.FileMajorPart, ".", $version.FileMinorPart, ".", $version.FileBuildPart))

        # the Github release (ghr)..
        $arguments = @("addasset", "-t", $Env:CIRCLE_TAG, "--token", $Env:GITHUB_TOKEN, "-o", "VPKSoft", "-r", $Env:CIRCLE_PROJECT_REPONAME, "-a", $file)
        & $gitreleasemanager $arguments

	    Write-Output (-join("Package released:", $file, "."))
    }
    Write-Output "Release."
}
else
{
    Write-Output (-join("No TAG detected, no asset publish."))
}
