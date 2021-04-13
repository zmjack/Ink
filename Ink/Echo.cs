using System;
using System.Collections.Generic;

namespace Ink
{
    public static class Echo
    {
        public static ConOut Instance = new ConOut();

        public static ConOut ClearRow() => Instance.ClearRow();
        public static ConOut RowBeginning() => Instance.RowBeginning();
        public static ConOut RowEnd() => Instance.RowEnd();

        public static ConOut Line() => Instance.Line();
        public static ConOut Line(string content, ConColor color = null) => Instance.Line(content, color);
        public static ConOut Print(string content, ConColor color = null) => Instance.Print(content, color);
        public static ConOut Left(string line, ConColor color = null) => Instance.Left(line, color);
        public static ConOut Center(string line, ConColor color = null) => Instance.Center(line, color);
        public static ConOut Right(string line, ConColor color = null) => Instance.Right(line, color);
        public static ConOut CoverLeft(string line, ConColor color = null) => Instance.CoverLeft(line, color);
        public static ConOut CoverCenter(string line, ConColor color = null) => Instance.CoverCenter(line, color);
        public static ConOut CoverRight(string line, ConColor color = null) => Instance.CoverRight(line, color);
        public static ConOut Row(string[] cols, int[] colLengths) => Instance.Row(cols, colLengths);

        public static ConOut Move(int offsetRow, int offsetCol) => Instance.Move(offsetRow, offsetCol);
        public static ConOut RowMove(int offsetRow) => Instance.RowMove(offsetRow);
        public static ConOut ColMove(int offsetCol) => Instance.ColMove(offsetCol);

        public static ConOut Table<TModel>(IEnumerable<TModel> models) => Instance.Table(models);
        public static ConOut Table(string[] headers, string[][] colLines, int[] lengths) => Instance.Table(headers, colLines, lengths);
        public static ConOut NoBorderTable<TModel>(IEnumerable<TModel> models) => Instance.NoBorderTable(models);
        public static ConOut NoBorderTable(string[] headers, string[][] colLines, int[] lengths) => Instance.NoBorderTable(headers, colLines, lengths);
        public static ConOut SeamlessTable<TModel>(IEnumerable<TModel> models) => Instance.SeamlessTable(models);
        public static ConOut SeamlessTable(string[] headers, string[][] colLines, int[] lengths) => Instance.SeamlessTable(headers, colLines, lengths);

        public static ConOut Ask<T>(string question, out T value, Action<AskAnswer> followResolve) => Instance.Ask(question, out value, followResolve);
        public static ConOut Ask<T>(string question, out T value) => Instance.Ask(question, out value);
        public static ConOut Ask<T>(string question, out T value, T defaultValue) => Instance.Ask(question, out value, defaultValue);
        public static ConOut Ask(string question, out string value, string endsWith, bool includeEndsWith) => Instance.Ask(question, out value, endsWith, includeEndsWith);
        public static ConOut Ask(string question, out string value, string endsWith, bool includeEndsWith, string defaultValue) => Instance.Ask(question, out value, endsWith, includeEndsWith, defaultValue);

        public static ConOut AskYN(string question, out bool value) => Instance.AskYN(question, out value);
        public static ConOut PressContinue() => Instance.PressContinue();
        public static ConOut PressContinue(ConsoleKey key) => Instance.PressContinue(key);
        public static ConOut PressContinue(ConsoleModifiers modifiers, ConsoleKey key) => Instance.PressContinue(modifiers, key);
    }
}
