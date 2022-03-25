A new HTTP handler has been configured in your application for consulting the
error log and its feeds. It is reachable at elmah.axd under your application 
root. If, for example, your application is deployed at http://www.example.com,
the URL for ELMAH would be http://www.example.com/elmah.axd. You can, of
course, change this path in your application's configuration file.

ELMAH is also set up to be secure such that it can only be accessed locally.
You can enable remote access but then it is paramount that you secure access
to authorized users or/and roles only. This can be done using standard
authorization rules and configuration already built into ASP.NET. For more
information, see http://code.google.com/p/elmah/wiki/SecuringErrorLogPages on
the project site.

Please review the commented out authorization section under
<location path="elmah.axd"> and make the appropriate changes.
