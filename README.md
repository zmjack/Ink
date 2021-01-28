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
    .Line("+==++==++==++==++==++==+");
```

> Welcome to use Ink  
> +==++==++==++==++==++==+

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

#### Multicolumn formatted output

```C#
Echo.Row(new[] { "ColA", "ColB" }, new[] { 10, 20 });
```

<br/>

### Table output

---

#### Border table

```C#
Echo.BorderTable(
    headers: new[] { "ColA", "ColB" },
	colLines: new[] {
        new[] { "A0", "B0" }
    },
    lengths : new[] { 10, 20 });
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

#### Seamless table

```C#
Echo.SeamlessTable(
    headers: new[] { "ColA", "ColB" },
	colLines: new[] {
        new[] { "A0", "B0" }
    },
    lengths : new[] { 10, 20 });
```

<br/>

### Interaction

---

#### Ask Yes / No

```C#
bool answer;
Echo.AskYN("Are you sure", yn => answer = yn)
    .Line($"The answer: {answer}");
```

**Yes**（y, yes, Y, Yes, YES）：

> ? Are you sure: Yes  
> The answer: true

**No**（n, no, N, No, NO）：

> ? Are you sure: No  
> The answer: false

#### Ask

```C#
string name;
Echo.Ask("Input your name", _name => name = name)
    .Line($"Your name is {name}.");
```

> ? Input your name: jack  
> Your name is jack.

<br/>

### Cursor

---

#### Set cursor position

```C#
// set cursor to (row 1, col 10)
Echo.Offset(1, 10);
```

