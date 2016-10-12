# Intro
This repository contains some utils functions for Sitecore. You can use the full project or just extract the functions you want easily.

# The functions

## StringExtensions
### SanitizeQuery
Sanitize an XPath query. This method encapulate the query segments by # # to avoid the errors with the reserved keywords or the issues with the items who begin by 0

Example: 
```csharp
var myQuery = "/sitecore/content/TestSites/Test/09/28/15/56/*".SanitizeQuery();
var item = Sitecore.Context.ContentDatabase.SelectSingleItem(myQuery);