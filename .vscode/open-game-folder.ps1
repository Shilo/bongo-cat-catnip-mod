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
$bongoCatPath = $null

foreach ($pg in $propsXml.Project.PropertyGroup) {
    if ([string]::IsNullOrWhiteSpace($steamPath)) {
        $steamPath = Get-PropertyValue $pg 'SteamPath'
    }
    if ([string]::IsNullOrWhiteSpace($bongoCatPath)) {
        $bongoCatPath = Get-PropertyValue $pg 'BongoCatPath'
    }
}

# Resolve MSBuild variable references in BongoCatPath
if (-not [string]::IsNullOrWhiteSpace($bongoCatPath)) {
    if ($bongoCatPath -match '\$\(SteamPath\)') {
        if ([string]::IsNullOrWhiteSpace($steamPath)) {
            Write-Error "BongoCatPath contains `$(SteamPath) but SteamPath is not configured."
            exit 1
        }
        $bongoCatPath = $bongoCatPath -replace '\$\(SteamPath\)', $steamPath
    }
}

# Use default if not configured
if ([string]::IsNullOrWhiteSpace($bongoCatPath)) {
    if ([string]::IsNullOrWhiteSpace($steamPath)) {
        $steamPath = 'C:\Program Files (x86)\Steam'
    }
    $bongoCatPath = Join-Path $steamPath 'steamapps\common\BongoCat'
}

# Validate path exists
if (-not (Test-Path -LiteralPath $bongoCatPath)) {
    Write-Error "BongoCat folder was not found at $bongoCatPath."
    exit 1
}

# Open folder in Explorer
Write-Host "Opening game folder: $bongoCatPath"
Invoke-Item -LiteralPath $bongoCatPath

