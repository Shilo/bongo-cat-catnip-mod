param(
    [string]$Workspace = (Get-Item -LiteralPath ".").FullName
)

# Find props file (prefer Local over Build)
$propsPath = Join-Path $Workspace 'Directory.Local.props'
if (-not (Test-Path -LiteralPath $propsPath)) {
    $propsPath = Join-Path $Workspace 'Directory.Build.props'
    if (-not (Test-Path -LiteralPath $propsPath)) {
        Write-Error "Neither Directory.Local.props nor Directory.Build.props was found in $Workspace."
        exit 1
    }
}

# Read XML
try {
    [xml]$propsXml = Get-Content -LiteralPath $propsPath
} catch {
    Write-Error "Failed to read props file: $_"
    exit 1
}

# Extract properties from PropertyGroup elements
function Get-PropertyValue($propertyGroup, $propertyName) {
    $prop = $propertyGroup.$propertyName
    if ($null -eq $prop) { return $null }
    if ($prop -is [System.Xml.XmlElement]) {
        return $prop.InnerText.Trim()
    }
    return $prop.ToString().Trim()
}

$steamPath = $null
$steamAppId = $null

foreach ($pg in $propsXml.Project.PropertyGroup) {
    if ([string]::IsNullOrWhiteSpace($steamPath)) {
        $steamPath = Get-PropertyValue $pg 'SteamPath'
    }
    if ([string]::IsNullOrWhiteSpace($steamAppId)) {
        $steamAppId = Get-PropertyValue $pg 'BongoCatSteamAppId'
    }
}

# Use default SteamPath if not configured
if ([string]::IsNullOrWhiteSpace($steamPath)) {
    $steamPath = 'C:\Program Files (x86)\Steam'
}

# Use default app ID if not configured
if ([string]::IsNullOrWhiteSpace($steamAppId)) {
    $steamAppId = '3419430'
}

# Launch via Steam
$steamExe = Join-Path $steamPath 'steam.exe'
if (-not (Test-Path -LiteralPath $steamExe)) {
    Write-Error "steam.exe was not found at $steamExe."
    exit 1
}

Write-Host "Launching BongoCat via Steam (App ID: $steamAppId)"
Start-Process -FilePath $steamExe -ArgumentList "-applaunch", $steamAppId

