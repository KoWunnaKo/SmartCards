﻿using SmartCardApi.Infrastructure;

namespace SmartCardApi.SecureMessaging.Pipe
{
    public class StepBytesCount : INumber
    {
        private readonly INumber _stepLength;
        private readonly INumber _allBytesCount;
        private readonly INumber _lastByteIndex;
        public StepBytesCount(
                INumber stepLength,
                INumber allBytesCount,
                INumber lastByteIndex
            )
        {
            _stepLength = stepLength;
            _allBytesCount = allBytesCount;
            _lastByteIndex = lastByteIndex;
        }
        public int Value()
        {
            return 
                new Sum(
                    _lastByteIndex, 
                    _stepLength
                ).Value() < _allBytesCount.Value() ? _stepLength.Value() : _allBytesCount.Value() % _stepLength.Value();
        }
    }
}
