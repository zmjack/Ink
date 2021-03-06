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

```C#
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

```C#
Echo.Print("text.");
```

#### 输出单行文本

```C#
Echo.Line("Single line.");
```

#### 行左侧输出 / 行中输出 / 行右侧输出

```C#
Echo.Left("Left");
Echo.Center("Center");
Echo.Right("Right");
```

#### 多列格式化输出

```C#
Echo.Row(new[] { "ColA", "ColB" }, new[] { 10, 20 });
```

```
ColA        ColB
```

<br/>

### 表格输出

---

#### 字符边框表格

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

#### 制表符边框表格

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

#### 无边框表格

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

```C#
bool answer;
Echo.AskYN("Are you sure", yn => answer = yn)
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

```C#
string name;
Echo.Ask("Input your name", answer => name = answer)
    .Line($"Your name is {name}.");
```

```
? Input your name: jack
Your name is jack.
```

<br/>

