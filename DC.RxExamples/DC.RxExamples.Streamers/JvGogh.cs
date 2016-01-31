//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reactive.Linq;
//using System.Text;

namespace DC.RxExamples.Streamers
{
    public static class JvGogh
    {
    //    public static IEnumerable<IObservable<object>> AsyncReadHelper(IObserver<byte[]> result, Stream stream, int bufferSize)
    //    {
    //        var asyncRead = Observable.FromAsyncPattern<byte[], int, int, int>(stream.BeginRead, stream.EndRead);
    //        var buffer = new byte[bufferSize];



    //        while (true)
    //        {
    //            var read = asyncRead(buffer, 0, bufferSize).Start();
    //            yield return read;
    //            // As asyncRead returns an AsyncSubject, 
    //            // and the Iterator operator takes care of exceptions, 
    //            // we know at this point there will always be exactly one value in the list.


    //            var bytesRead = read[0];
    //            if (bytesRead == 0)
    //            {
    //                // End of file       
    //                yield break;
    //            }
    //            var outBuffer = new byte[bytesRead];
    //            Array.Copy(buffer, outBuffer, bytesRead);

    //            // Fire out to the outer Observable 
    //            result.OnNext(outBuffer);
    //        }
    //    }

    //    public static IObservable<string> AsyncReadLines(
    //this Stream stream, int bufferSize)
    //    {
    //        return Observable.CreateWithDisposable<string>(observer =>
    //        {
    //            var sb = new StringBuilder();
    //            var blocks = AsyncRead(stream, bufferSize).Select(
    //                block => Encoding.ASCII.GetString(block));

    //            Action produceCurrentLine = () =>
    //            {
    //                var text = sb.ToString();
    //                sb.Length = 0;
    //                observer.OnNext(text);
    //            };

    //            return blocks.Subscribe(data =>
    //            {
    //                for (var i = 0; i < data.Length; i++)
    //                {
    //                    var atEndofLine = false;
    //                    var c = data[i];
    //                    if (c == '\r')
    //                    {
    //                        atEndofLine = true;
    //                        var j = i + 1;
    //                        if (j < data.Length && data[j] == '\n')
    //                            i++;
    //                    }
    //                    else if (c == '\n')
    //                    {
    //                        atEndofLine = true;
    //                    }
    //                    if (atEndofLine)
    //                    {
    //                        produceCurrentLine();
    //                    }
    //                    else
    //                    {
    //                        sb.Append(c);
    //                    }
    //                }
    //            },
    //            observer.OnError,
    //            () =>
    //            {
    //                produceCurrentLine();
    //                observer.OnCompleted();
    //            });
    //        });
    //    }
    }
}