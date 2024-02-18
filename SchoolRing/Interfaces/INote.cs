using System;

namespace SchoolRing.Interfaces
{
    public interface INote
    {
        string Text { get; }
        DateTime Date { get; }
        DateTime DateCreated { get; }
        int ClassNum { get; }
        bool Purva { get; }
    }
}
