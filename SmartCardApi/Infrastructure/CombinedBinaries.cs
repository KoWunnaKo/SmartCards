﻿using System.Collections.Generic;
using System.Linq;
using SmartCardApi.Infrastructure.Interfaces;

namespace SmartCardApi.Infrastructure
{
    public class CombinedBinaries : IBinary
    {
        private readonly IBinary _a;
        private readonly IBinary _b;
        private readonly IBinary _c;
        private readonly IBinary _d;

        public CombinedBinaries(
                IEnumerable<byte> a,
                IEnumerable<byte> b
            ) : this(new Binary(a), new Binary(b), new Binary(), new Binary()) { }

        public CombinedBinaries(
                byte[] a,
                byte[] b
            ) : this(new Binary(a), new Binary(b), new Binary(), new Binary()) { }

        public CombinedBinaries(
                IBinary a,
                IBinary b
            ) : this(a, b, new Binary(), new Binary()) { }

        public CombinedBinaries(
                IBinary a,
                IBinary b,
                IBinary c
            ) : this(a, b, c, new Binary()) { }

        public CombinedBinaries(
                IBinary a,
                IBinary b,
                IBinary c,
                IBinary d
            )
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
        }
        public byte[] Bytes()
        {
            return _a
                    .Bytes()
                    .Concat(
                        _b.Bytes()
                    )
                    .Concat(
                        _c.Bytes()
                    )
                    .Concat(
                        _d.Bytes()
                    )
                    .ToArray();
        }
    }
}
