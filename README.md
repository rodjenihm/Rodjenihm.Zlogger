# Rodjenihm.Zlogger
Simple Keylogger written in C#.  
.NET Framework 4.6.1  

7.31.2019 - Version 1.0 logs KeyDown events in log.txt file. Needs a lot of improvements.  
8.2.2019 - Version 2.0 initially logs keys in StringBuilder buffer. If buffer is not empty program creates log files periodically defined by Interval property of Keylogger class.  
8.4.2019 - Zlogger can now send logs via email without saving them to file system. Just use HandleIntervalElapsed_SendEmail as timer event. In order to use this option you will need your smtp login credentials.


This program logs virtual key codes. Two reasons: 1) It's faster to log down VkCode; 2) Log files cannot be read without previous "decoding". For "decoding" (using "" becuase there is really nothing to decode, it's simple conversion from vkCode to key) I will create special program that will take log file as input and output it in readable format.  

In progress, will be improved in the future...
