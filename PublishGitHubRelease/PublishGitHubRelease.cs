using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using Octokit;

namespace PublishGitHubRelease
{
	[Cmdlet("Publish", "GitHubRelease")]
    public class PublishGitHubReleaseCmdlet : PSCmdlet
    {

		[Parameter(Mandatory = true, Position = 1)]
		public string ApplicationName { get; set; }

		[Parameter(Mandatory = true)]
		public string Token { get; set; }

		[Parameter(Mandatory = true)]
		public string Repo { get; set; }

		[Parameter(Mandatory = true)]
		public string Owner { get; set; }

		[Parameter(Mandatory = true)]
		public string TagName { get; set; }

		[Parameter(Mandatory = true)]
		public string ReleaseName { get; set; }

        [Parameter(Mandatory = false)]
        public string TargetCommitish { get; set; }

        [Parameter(Mandatory = false)]
		public string ReleaseBody { get; set; }

		[Parameter(Mandatory = true)]
		public bool Draft { get; set; }

		[Parameter(Mandatory = false)]
		public bool PreRelease { get; set; }

		[Parameter(Mandatory = true)]
		public string[] Assets { get; set; }

		protected override void ProcessRecord()
	    {
		    try
		    {
			    var client = new GitHubClient(new ProductHeaderValue(ApplicationName));
			    var tokenAuth = new Credentials(Token); 
			    client.Credentials = tokenAuth;

			    var newRelease = new NewRelease(TagName)
			    {
				    Name = ReleaseName,
                    TargetCommitish = TargetCommitish,
                    Body = ReleaseBody,					
				    Draft = Draft,
				    Prerelease = PreRelease,
			    };


			    var release = client.Repository.Release.Create(Owner, Repo, newRelease).Result;
			    WriteVerbose("Created release with id: " + release.Id + " at " + release.Url);
				WriteVerbose("Uploading " + Assets.Count() + " assets");
			    foreach (var asset in Assets)
			    {
					WriteVerbose("Uploading " + asset);
				    using (var archiveContents = File.OpenRead(asset))
				    {
					    var fileName = Path.GetFileName(asset);
					    var assetUpload = new ReleaseAssetUpload()
					    {
						    FileName = fileName,
						    ContentType = "application/zip",
						    RawData = archiveContents
					    };

					    var uploadedAsset = client.Repository.Release.UploadAsset(release, assetUpload).Result;
						WriteVerbose("Uploaded " + uploadedAsset.Name);
				    }
			    }
		    }
		    catch (Exception e)
		    {
				WriteVerbose("Error when publishing release: " + e);
			    throw;
		    }
	    }

    }
}
