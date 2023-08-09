SET CSDocument="C:\Program Files (x86)\CSPro 8.0\CSDocument.exe"

cd /d %~dp0
del /q /f /s "Outputs\PDF" 

%CSDocument% -build "PDF Documentation" -input "CSEntry\CSEntry.csdocset" "CSPro\CSPro.csdocset" "CSWeb\CSWeb.csdocset" "GetStart\GetStart.csdocset"
