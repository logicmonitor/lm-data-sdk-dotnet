#!/bin/sh
cd /lm-data-sdk-dotnet
dotnet restore 
dotnet build
ls -l
chown -R 2007:2007 LogicMonitor.DataSDK/bin
chown -R 2007:2007 LogicMonitor.DataSDK/obj
ls -l
