# Description

This extension currently ships with one build/release task that integrates with GitHub, but more are planned.

>If you have any suggestions on build tasks that integrates with GitHub, please submit an issue on the GitHub site

## Publish GitHub Release
This task lets you publish the output of a build to GitHub. The task will create a new release in the selected GitHub repository and upload any files that you specify as assets to the release.

The task allows you to create a release in a GitHub account using a Personal Access Token (PAT) that you generate over at GitHub. Please see [https://help.github.com/articles/creating-an-access-token-for-command-line-use/](https://help.github.com/articles/creating-an-access-token-for-command-line-use) for more information

You can specify the following fields in the task:

* Release Name
* TagName
* Release Body (MarkDown)
* Draft
* Prerelease
* Release Assets
* TargetCommitish


# Documentation
Please check the [Wiki](https://github.com/jakobehn/vstsgithubtasks/wiki) for more information.