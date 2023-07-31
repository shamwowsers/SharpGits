#!/bin/bash
docker run --rm -it -v $(pwd):/app/ -w /app mcr.microsoft.com/dotnet/sdk:6.0
