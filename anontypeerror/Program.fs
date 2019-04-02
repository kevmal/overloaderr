
open Microsoft.StreamProcessing
open System
open System.Linq.Expressions

type Record(a : int) = 
    member x.A = a

let f (x : IStreamable<int64, Record>) : IStreamable<int64,int> = x.Select(fun x -> x.A) //Error
let g (x : IStreamable<int64, Record>) = Microsoft.StreamProcessing.Streamable.Select(x, (fun x -> x.A)) //Error
let h (x : IStreamable<int64, Record>) = Microsoft.StreamProcessing.Streamable.Select<int64, Record, int>(x, (fun x -> x.A)) //Error
let k (x : IStreamable<int64, Record>) = x.Select(selector = (fun x -> x.A)) //Okay

let inline helper (f : Expression<Func<'a, _>>) (x : IStreamable<int64, 'a>)  = x.Select(f)

type Bleh() =
    static member Expr(v : Expression<Func<'a,'b>>) = v
let l (x : IStreamable<int64, Record>) = x |> helper (Bleh.Expr(fun x -> x.A)) //Okay
