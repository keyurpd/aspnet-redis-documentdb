# ASP.NET Core Sample Website

> Note: This code tries to connect to Azure DocumentDb and Redis services.
> Kindly create/replace the connection strings in config.json file.
> Or use environment variables.

This is a sample asp.net web application targetting the .NET Core runtime.

Following are the steps to be followed.
 - Install .NET Core 1.0 / .NET 4.5.1
 - Install Node, NPM, Yeoman
 - Install ASP.NET generator for Yeoman
 - Create the first ASP.NET web app using Yeoman scaffolding
 - Change the code to meet the requirement
 - Run one of the web commands to bring the site up
 - Browse the site through the browser

It uses following features of ASP.NET Core
 - Targets ASP.NET Core rc1 update1, except Redis and DocumentDb API
 - Uses built-in ASP.NET Dependency Injection
 - Uses new Option Model / Env Vars instead of web.config
 - Retrieves data from Azure DocumentDb
 - Uses Cache-Aside strategy for caching in Redis

(C) Keyur