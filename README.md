Recursive knight (rknight)
====

Exercise in recursive programming from my first university courses back in the day solving a knight's journey across a chess board. It must visit each square once and only once.

Four implementations available
* C# - .NET Core
* TypeScript for node
* Go
* Python

The applications take three optional parameters:
1. Size of the board itself.
2. Start column number. 0 = A, 1 = B, etc.
3. Start row number 0 = 1.

Default is a 7x7 board, and the starting position is A1. I.e. the equivalent of `7 0 0` as command line arguments.


.NET Core 
----

How to build and run:

```
cd csharp
dotnet run -c Release [arguments]
```

TypeScript
---

Build:

```
cd js
npm i
npm run build
```

Run:

```
node dist/main.js [arguments]
```
or
```
npm run [arguments]
```

Go
----

How to build and run:

```
cd golang
go build rknight.go
./rknight [arguments]
```

Python
------

In Python, all three arguments must be supplied. Argparse requires positional
arguments to always be there.

```
cd python
python rknight.py 7 0 0
```


Informal performance comparison
===============================

To find a solution on an 8x8 board takes a lot of time. Too long
some would say. A more suitable performance test would be on an
7x7 board starting from B2.

The current tests are indicative. Only run nce on a Dell XPS with Intel i7-7500U
2.7 GHz running Ubuntu 18.04.3 LTS.

Timing was done with `time`. Reported running time is the resulting *User*
contribution. It thus includes some startup time and other non-productive
tasks, which seemed to be mostly significant for .NET, but that's how the 
programs will be run in real life and thus relevant from a user's perspective.

| Language   | Version           | Time [sec] |
|------------|-------------------|-----------:|
| Go         | 1.10.4            |   4.61     |
| C#         | .NET Core 3.0.100 |   6.44     |
| JavaScript | Node 10.16.0      |   8.55     |
| Python     | 2.7.15+ (GCC 7.4) | 281.21     |
| Python     | 3.6.8 (GCC 8.3)   | 322.60     |
| Python     | 3.7.4 (GCC 7.3)   | 355.05     |
