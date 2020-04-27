using System;
using System.Threading.Tasks;

namespace CommandSample.Domain.Models
{
    public struct Nothing : IEquatable<Nothing>, IComparable<Nothing>, IComparable
    {
        public static readonly Nothing Value = new Nothing();

        public static readonly Task<Nothing> Task = System.Threading.Tasks.Task.FromResult(Value);

        public int CompareTo(Nothing other) => 0;

        int IComparable.CompareTo(object obj) => 0;

        public override int GetHashCode() => 0;

        public bool Equals(Nothing other) => true;

        public override bool Equals(object obj) => obj is Nothing;

        public static bool operator ==(Nothing first, Nothing second) => true;

        public static bool operator !=(Nothing first, Nothing second) => false;

        public override string ToString() => "()";
    }
}
