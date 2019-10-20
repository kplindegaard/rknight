Recursive knight (rknight)
====

Exercise in recursive programming from my first university courses back in the day solving a knight's journey across a chess board. It must visit each square once and only once.

Three implementations available
* C# - .NET Core
* TypeScript for node
* Go

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
