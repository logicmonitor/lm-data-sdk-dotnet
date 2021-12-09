#!/bin/sh
cd /lm-data-sdk-dotnet
dotnet restore 
dotnet build
ls -l
chown -R 2007:2007 LogicMonitor.DataSDK
chown -R 2007:2007 LogicMonitor.DataSDK.Tests

ls -l
