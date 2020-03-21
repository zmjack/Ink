using EchoSharp;
using System;
using System.Collections.Generic;

namespace EchoSharp
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
        public static ConOut Right(string line, ConColor color = null) => Instance.Right(line, color);
        public static ConOut Center(string line, ConColor color = null) => Instance.Center(line, color);
        public static ConOut Row(string[] cols, int[] colLengths) => Instance.Row(cols, colLengths);

        public static ConOut Offset(int offsetRow, int offsetCol) => Instance.Offset(offsetRow, offsetCol);
        public static ConOut OffsetRow(int offsetRow) => Instance.OffsetRow(offsetRow);
        public static ConOut OffsetCol(int offsetCol) => Instance.OffsetCol(offsetCol);

        public static ConOut BorderTable<TModel>(IEnumerable<TModel> models) => Instance.BorderTable(models);
        public static ConOut BorderTable(string[] headers, string[][] colLines, int[] lengths) => Instance.BorderTable(headers, colLines, lengths);
        public static ConOut NoBorderTable<TModel>(IEnumerable<TModel> models) => Instance.NoBorderTable(models);
        public static ConOut NoBorderTable(string[] headers, string[][] colLines, int[] lengths) => Instance.NoBorderTable(headers, colLines, lengths);
        public static ConOut SeamlessTable<TModel>(IEnumerable<TModel> models) => Instance.SeamlessTable(models);
        public static ConOut SeamlessTable(string[] headers, string[][] colLines, int[] lengths) => Instance.SeamlessTable(headers, colLines, lengths);

        public static ConOut Ask(string question, Func<string, string> resolver) => Instance.Ask(question, resolver);
        public static ConOut AskYN(string question, Func<bool, string> resolver) => Instance.AskYN(question, resolver);
        public static ConOut AskYN(string question, Action<bool> method) => Instance.AskYN(question, method);
        public static ConOut AskYN(string question, out bool ret) => Instance.AskYN(question, out ret);

    }
}
