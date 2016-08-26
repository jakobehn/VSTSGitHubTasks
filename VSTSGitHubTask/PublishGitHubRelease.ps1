[cmdletbinding()]
Param(
	[string]$applicationName,
	[string]$token,
	[string]$repo,
	[string]$owner,
	[string]$targetCommitish,
	[string]$tagName,
	[string]$releaseName,
	[string]$releaseBody,
	[string]$draft,
	[string]$prerelease,
	[string]$assetsPattern
)
Write-Verbose -Verbose "Entering script PublishRelease.ps1"
Write-Verbose -Verbose "applicationName = $applicationName"
Write-Verbose -Verbose "token = $token"
Write-Verbose -Verbose "repo = $repo"
Write-Verbose -Verbose "TargetCommitish = $targetCommitish"
Write-Verbose -Verbose "owner = $owner"
Write-Verbose -Verbose "tagName = $tagName"
Write-Verbose -Verbose "releaseName = $releaseName"
Write-Verbose -Verbose "releaseBody = $releaseBody"
Write-Verbose -Verbose "draft = $draft"
Write-Verbose -Verbose "prerelease = $prerelease"
Write-Verbose -Verbose "assetsPattern = $assetsPattern"

[bool]$draftBool= Convert-String $draft Boolean
[bool]$prereleaseBool= Convert-String $prerelease Boolean

# Import the Task.Common and Task.Internal dll that has all the cmdlets we need for Build
import-module "Microsoft.TeamFoundation.DistributedTask.Task.Internal"
import-module "Microsoft.TeamFoundation.DistributedTask.Task.Common"


$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$pathToModule = Join-Path $scriptDir "VSTSGitHub.dll"
import-module $pathToModule

$assets = Find-Files -SearchPattern $assetsPattern

Publish-GitHubRelease -ApplicationName $applicationName -Token $token -Repo $repo -Owner $owner -TagName $tagName -ReleaseName $releaseName -ReleaseBody $releaseBody -Draft $draftBool -PreRelease $prereleaseBool -TargetCommitish $targetCommitish -Assets $assets
