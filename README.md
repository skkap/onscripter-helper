# Onscripter Helper

Onscripter Helper is a tool which takes original Japanese phrases from a comments of 'nscript.dat' file and replaces English translation with it.

This tool was made for "When They Cry 4 / Umineko no Naku Koro ni Chiru/ うみねこのなく頃に散" visual novel, but probably will work with others Onscripter games (with some changes).

# About ONScripter

NScripter is a visual novel engine written by Naoki Takahashi.

The best-known NScripter clone is the free software implementation, ONScripter. Its popularity among the visual novel localisation community is attributed to the ease of modifying the engine to support languages other than Japanese. It strives to maintain compatibility with visual novels designed for NScripter.

ONScripter is based on the Simple Directmedia Layer (SDL) library, and can thus be used to run NScripter games on platforms supported by SDL, such as Mac OS X, Linux, Dreamcast, PlayStation Portable and the Apple iPod.

## NScript API reference
In Japanese: [http://senzogawa.s90.xrea.com/reference/NScrAPI.html](http://senzogawa.s90.xrea.com/reference/NScrAPI.html)

In English: [http://unclemion.com/onscripter/api/NScrAPI.html](http://unclemion.com/onscripter/api/NScrAPI.html)

# Usage

Just run 'onscripter-helper.exe' with a path to the decrypted (plain text in Japanese Shift-JIS encoding) 'nscript.dat' as an argument.

You can decrypt 'nscript.dat' with 'NSDEC.exe' and crypt back with 'nscmaker'.

 ```
jpgame.exe ./data/nscript.dat
 ```
