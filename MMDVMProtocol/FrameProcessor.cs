using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MMDVMProtocol
{
    public class FrameProcessor
    {
        private readonly ConcurrentQueue<byte> _values = new();
        private readonly Queue<Frame> _pendingFrames = new();
        private Task _processTask;
        private CancellationTokenSource _processCts;

        public void Insert(byte value) => _values.Enqueue(value);

        public bool TryTakeFrame(out Frame frame) => _pendingFrames.TryDequeue(out frame);
        
        public Frame DequeueFrame(CancellationToken cancellationToken = default)
        {
            bool success;
            Frame value;

            do
            {
                if (cancellationToken.IsCancellationRequested) throw new TaskCanceledException();
                
                success = TryTakeFrame(out value);
            } while (!success);
            
            return value;
        }

        public void Start()
        {
            if (_processTask != null) return;

            _processCts = new CancellationTokenSource();
            _processTask = Task.Run(Process);
        }

        public void Stop()
        {
            if (_processTask == null || _processCts == null) return;
            
            _processCts.Cancel();

            while (_processTask.Status == TaskStatus.Running) Thread.Sleep(10);

            _processTask = null;
            _processCts = null;
        }

        private byte Dequeue()
        {
            bool success;
            byte value;

            do
            {
                if (_processCts.IsCancellationRequested) throw new TaskCanceledException();
                
                success = _values.TryDequeue(out value);
            } while (!success);
            
            return value;
        }

        private void Process()
        {
            try
            {
                while (!_processCts.IsCancellationRequested)
                {
                    byte value = Dequeue();

                    if (value != ModemConstants.FrameStart) continue; //Need a frame start

                    Frame frame = new Frame();
                    frame.Length++; //1 byte for the frame start

                    PopulateFrame(frame);
                    
                    _pendingFrames.Enqueue(frame);
                }
            }
            catch (TaskCanceledException)
            {
                //Ignore
            }
        }

        private void PopulateFrame(Frame frame)
        {
            try
            {
                byte lengthByte = Dequeue();

                ushort dataLength = Convert.ToUInt16(lengthByte);

                frame.Length = dataLength;
                    
                dataLength--; //Frame start
                dataLength--; //Length byte
                dataLength--; //Command byte

                frame.Command = Dequeue();

                for (ushort dataPos = dataLength; dataPos > 0; dataPos--) frame.Data.Add(Dequeue());
            }
            catch (TaskCanceledException)
            {
                //Ignore
            }
        }
    }
}