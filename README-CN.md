# NEcho

NEcho 是个易于使用的用于控制控制台格式化输出的工具库。

- [English Readme](https://github.com/zmjack/NEcho/blob/master/README.md)
- [中文自述](https://github.com/zmjack/NEcho/blob/master/README-CN.md)



## 使用示例

NEcho 提供链式写法以简化批量输出操作。

```C#
Echo.Line("Welcome to use NEcho")
    .Line("+==++==++==++==++==++==+");
```

> Welcome to use NEcho  
>+==++==++==++==++==++==+



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

#### 多列输出

```C#
Echo.Row(new[] { "ColA", "ColB" }, new[] { 10, 20 });
```

---



### 表格输出

---

#### 字符边框表格

```C#
Echo.BorderTable(
    headers: new[] { "ColA", "ColB" },
	colLines: new[] {
        new[] { "A0", "B0" }
    },
    lengths : new[] { 10, 20 });
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

#### 制表符边框表格

```C#
Echo.SeamlessTable(
    headers: new[] { "ColA", "ColB" },
	colLines: new[] {
        new[] { "A0", "B0" }
    },
    lengths : new[] { 10, 20 });
```

---



### 输入交互

---

#### 询问 Yes/No

```C#
bool answer;
Echo.AskYN("Are you sure", yn => askYn = yn)
    .Line($"The answer: {answer}");
```

输入 y, yes, Y, Yes, YES，则判定为 Yes：

> ? Are you sure: Yes  
> The answer: true

输入  n, no, N, No, NO，则判定为 No：

> ? Are you sure: No  
> The answer: false

#### 询问

```C#
string filename;
Echo.Ask("Input filename", name => fileName = name)
    .Line($"The filename: {fileName}");
```

> ? Input filename: a.txt  
> The filename: a.txt

  

### 光标位置

---

#### 设置光标位置

```C#
// set cursor to (row 1, col 10)
Echo.Offset(1, 10);
```

