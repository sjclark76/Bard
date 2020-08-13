# StoryData

StoryData is a plain old C\# object \(POCO\) of your choice. An instance of this class will be instantiated at the beginning of your test arrangement and is accessible in every story. This means you can enrich your StoryData as your stories progress.

{% hint style="warning" %}
You can only have one instance of StoryData per configured scenario.
{% endhint %}

