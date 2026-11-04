.PHONY: help build clean restore publish run

# Default configuration
CONFIGURATION ?= Release
RUNTIME ?= win-x64
PROJECT = PrDashboard/PrDashboard.csproj
OUTPUT = publish

help:
	@echo PrDashboard Build Targets
	@echo.
	@echo   make build       - Clean build and publish
	@echo   make clean       - Clean build artifacts
	@echo   make restore     - Restore NuGet packages
	@echo   make publish     - Publish application
	@echo   make run         - Run the published application
	@echo.
	@echo Variables:
	@echo   CONFIGURATION    - Debug or Release (default: Release)
	@echo   RUNTIME          - Target runtime (default: win-x64)

build: clean restore publish
	@echo.
	@echo [OK] Build completed successfully!
	@echo Output: $(OUTPUT)/PrDashboard.exe

clean:
	@echo Cleaning build artifacts...
	@if exist PrDashboard\bin rd /s /q PrDashboard\bin
	@if exist PrDashboard\obj rd /s /q PrDashboard\obj
	@if exist $(OUTPUT) rd /s /q $(OUTPUT)
	@echo [OK] Clean completed

restore:
	@echo Restoring NuGet packages...
	@dotnet restore $(PROJECT) --runtime $(RUNTIME) --force --no-cache
	@echo [OK] Restore completed

publish:
	@echo Publishing application...
	@dotnet publish $(PROJECT) \
		--configuration $(CONFIGURATION) \
		--runtime $(RUNTIME) \
		--output $(OUTPUT) \
		--self-contained true \
		-p:PublishSingleFile=true \
		-p:IncludeNativeLibrariesForSelfExtract=true \
		-p:EnableCompressionInSingleFile=true \
		-p:DebugType=None \
		-p:DebugSymbols=false
	@echo [OK] Publish completed

run:
	@if not exist "$(OUTPUT)\PrDashboard.exe" ( \
		echo Error: PrDashboard.exe not found. Run 'make build' first. && \
		exit /b 1 \
	)
	@cd $(OUTPUT) && PrDashboard.exe
