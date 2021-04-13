# Ink

Ink 是个易于使用的用于控制控制台格式化输出的工具库。

- [English Readme](https://github.com/zmjack/Ink/blob/master/README.md)
- [中文自述](https://github.com/zmjack/Ink/blob/master/README-CN.md)

<br/>

## 安装

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

## 使用示例

**Ink** 提供链式写法以简化批量输出操作。

```csharp
Echo.Line("Welcome to use Ink")
    .Line("==================");
```

```
Welcome to use Ink
==================
```

<br/>

### 文本输出

---

#### 光标处输出文本

```csharp
Echo.Print("text.");
```

#### 输出单行文本

```csharp
Echo.Line("Single line.");
```

#### 行左侧输出 / 行中输出 / 行右侧输出

```csharp
Echo.Left("Left");
Echo.Center("Center");
Echo.Right("Right");
```

**覆盖输出：**

```csharp
Echo.CoverLeft("Left").CoverCenter("Center").CoverRight("Right");
```

```
Left        Center        Right
```

####

#### 多列格式化输出

```csharp
Echo.Row(new[] { "ColA", "ColB" }, new[] { 10, 20 });
```

```
ColA        ColB
```

<br/>

### 表格输出

---

#### 字符边框表格

```csharp
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

#### 制表符边框表格

```csharp
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

#### 无边框表格

```csharp
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

#### 对象输出

```
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

### 输入交互

---

#### 询问 Yes / No

```csharp
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

#### 询问

```csharp
Echo.Ask("Input your name:", out string name)
    .Ask("Age:", out int age)
    .Line($"Your name is {name} (Age: {age}).");
```

```
? Input your name: jack
? Age: 30
Your name is jack (Age: 30).
```

#### 询问多行

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

