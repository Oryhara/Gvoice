
set destination=7372284999
set messages=.help:".info help":".info cell":".info gps":".info fw":".info vcore":".info ign":".info obd":".info battery":".info sms":".info wifi":".info failure":".info <invalid>":".info testing":".info <blank>":".packageList":".status":".info c":".status c":".devhelp":".getlogs"

GoogleVoice.Net.exe -m %messages% -d %destination% > sms_log_%destination%_%date:~-4,4%%date:~-7,2%%date:~-10,2%_%time:~-11,2%%time:~-8,2%.txt
pause