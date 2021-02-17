@echo off
echo * Setting up env variables...
set CEF_VERSION=75.1.16+g16a67c4+chromium-75.0.3770.100

echo * Creating directories...
if not exist ".\obs-studio\" mkdir .\obs-studio
if not exist ".\libobs\" mkdir .\libobs
cd libobs

echo * Cloning libobs submodules...
git submodule update --init --recursive

echo * Downloading libobs dependencies:
echo[
echo[

echo ==== [ Visual Studio 2019 Dependencies ] ==================
if exist dependencies2019.zip (
	curl -kLO https://cdn-fastly.obsproject.com/downloads/dependencies2019.zip -f --retry 5 -z dependencies2019.zip
) else (
	curl -kLO https://cdn-fastly.obsproject.com/downloads/dependencies2019.zip -f --retry 5 -C -
)

echo[ 
echo ==== [ VLC Dependencies ] ==================
if exist vlc.zip (
	curl -kLO https://cdn-fastly.obsproject.com/downloads/vlc.zip -f --retry 5 -z vlc.zip
) else (
	curl -kLO https://cdn-fastly.obsproject.com/downloads/vlc.zip -f --retry 5 -C -
)

echo[
echo ==== [ Chrome Embedded Framework Dependencies ] ==================
if exist cef_binary_%CEF_VERSION%_windows64_minimal.zip (
	curl -kLO https://cdn-fastly.obsproject.com/downloads/cef_binary_%CEF_VERSION%_windows64_minimal.zip -f --retry 5 -z cef_binary_%CEF_VERSION%_windows64_minimal.zip
) else (
	curl -kLO https://cdn-fastly.obsproject.com/downloads/cef_binary_%CEF_VERSION%_windows64_minimal.zip -f --retry 5 -C -
)

echo[
echo[

echo * Installing libobs dependencies:

echo[
echo[
echo ==== [ Visual Studio 2019 Dependencies ] ==================
..\utils\7z -y -bso0 -bsp1 x dependencies2019.zip -oobs-deps\dependencies2019
set DepsPath64=%CD%\obs-deps\dependencies2019\win64
echo Done.

echo[
echo ==== [ VLC Dependencies ] ==================
..\utils\7z -y -bso0 -bsp1 x vlc.zip -oobs-deps\vlc
set VLCPath=%CD%\obs-deps\vlc
echo Done.

echo[
echo ==== [ Chrome Embedded Framework Dependencies ] ==================
..\utils\7z -y -bso0 -bsp1 x cef_binary_%CEF_VERSION%_windows64_minimal.zip -oobs-deps\CEF_64
set CEF_64=%CD%\obs-deps\CEF_64\cef_binary_%CEF_VERSION%_windows64_minimal
echo Done.

echo[
echo[

echo * Running CMAKE...

echo[
echo[
cmake -G "Visual Studio 16 2019" -A x64 -DCMAKE_SYSTEM_VERSION=10.0 -DCOPIED_DEPENDENCIES=false -DCOPY_DEPENDENCIES=true -DENABLE_VLC=ON -DDISABLE_UI=true -DCOMPILE_D3D12_HOOK=true -DBUILD_BROWSER=true -DBROWSER_PANEL_SUPPORT_ENABLED=OFF -DCEF_ROOT_DIR=%CEF_64% ..\obs-studio\
echo[
echo[

echo * Running Build...

echo[
echo[
cd
cmake --build %CD% --target ALL_BUILD --config Release
cd ..
echo[
echo[

echo ****** COMPLETE ******