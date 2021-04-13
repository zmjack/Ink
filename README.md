# Ink

Ink is an easy-to-use library for controlling console formatting output.

- [English Readme](https://github.com/zmjack/Ink/blob/master/README.md)
- [中文自述](https://github.com/zmjack/Ink/blob/master/README-CN.md)

<br/>

## Install

- **Package Manager**

  ```powershell
  Install-Package Ink
  ```

- **.NET CLI**

  ```powershell
  dotnet add package Ink
  ```

- **PackageReference**

  ```powershell
  <PackageReference Include="Ink" Version="*" />
  ```

<br/>

## Samples

**Ink** provides chain writing to simplify output operations for more than one line.

```C#
Echo.Line("Welcome to use Ink")
    .Line("==================");
```

```
Welcome to use Ink
==================
```

<br/>

### Text output

---

#### Output text at the cursor

```c#
Echo.Print("text.");
```

#### Output single-line text

```c#
Echo.Line("Single line.");
```

#### Output text from Left / Center / Right

```c#
Echo.Left("Left");
Echo.Center("Center");
Echo.Right("Right");
```

**Overwrite output:**

```csharp
Echo.CoverLeft("Left").CoverCenter("Center").CoverRight("Right");
```

```
Left        Center        Right
```

#### Multicolumn formatted output

```C#
Echo.Row(new[] { "ColA", "ColB" }, new[] { 10, 20 });
```

```
ColA        ColB
```

<br/>

### Table output

---

#### Border table

```C#
Echo.Table(
    headers: new[] { "ColA", "ColB" },
	colLines: new[] {
        new[] { "A0", "B0" }
    },
    lengths : new[] { 10, 20 });
```

```
+------------+----------------------+
| ColA       | ColB                 |
+------------+----------------------+
| A0         | B0                   |
+------------+----------------------+
```

#### Seamless table

```C#
Echo.SeamlessTable(
    headers: new[] { "ColA", "ColB" },
	colLines: new[] {
        new[] { "A0", "B0" }
    },
    lengths : new[] { 10, 20 });
```

```
┌───────────┬─────────────────────┐
│ ColA      │ ColB                │
├───────────┼─────────────────────┤
│ A0        │ B0                  │
└───────────┴─────────────────────┘
```

#### No border table

```C#
Echo.NoBorderTable(
    headers: new[] { "ColA", "ColB" },
	colLines: new[] {
        new[] { "A0", "B0" }
    },
    lengths : new[] { 10, 20 });
```

```
ColA        ColB
A0          B0
```

#### Object Output

```csharp
Echo.Table(new[]
{
    new { Name = "Jack", Note = "Beer lover." },
});
```

```
+------+-------------+
| Name | Note        |
+------+-------------+
| Jack | Beer lover. |
+------+-------------+
```

<br/>

### Interaction

---

#### Ask Yes / No

```C#
Echo.AskYN("Are you sure:", out var answer)
    .Line($"The answer: {answer}");
```

**Yes**（y, yes, Y, Yes, YES）：

```
? Are you sure: Yes
The answer: true
```

**No**（n, no, N, No, NO）：

```
? Are you sure: No
The answer: false
```

#### Ask

```C#
Echo.Ask("Input your name:", out string name)
    .Ask("Age:", out int age)
    .Line($"Your name is {name} (Age: {age}).");
```

```
? Input your name: jack
? Age: 30
Your name is jack (Age: 30).
```

#### Ask for multiline

```csharp
Echo.Ask($"Mottos:{Environment.NewLine}", out var motto, endsWith: ";")
    .Line($"The answer: {motto.Replace(Environment.NewLine, " ")}");
```

```
? Mottos:
Life is but a span.
Write less, do more.;
The answer: Life is but a span. Write less, do more.
```

<br/>

