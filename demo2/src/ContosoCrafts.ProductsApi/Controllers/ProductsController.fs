namespace ContosoCrafts.ProductsApi.Controllers

open System.Threading.Tasks;
open Microsoft.AspNetCore.Mvc;

open ContosoCrafts.ProductsApi.Services;
// public class RatingRequest
// {
//     public string ProductId { get; set; }
//     public int Rating { get; set; }
// }

// assuming the framework will be ok with immutables here
// if not uncomment the attribute and try that
// [<CLIMutable>]
type RatingRequest = {ProductId: string; Rating: int}

[<ApiController>]
[<Route("[controller]")>]
type ProductsController(productService: IProductService) =
    inherit ControllerBase()

    [<HttpGet>]
    // public async Task<ActionResult> GetList(int page = 1, int limit = 20)
    member this.GetList(?page, ?limit) : Task<IActionResult> =
        let pagingInfo =
            match page, limit with
            | Some page, Some limit ->
                PagingInfo(page, limit)
            | Some page, None ->
                PagingInfo(page)
            | None, Some limit ->
                PagingInfo(limit= limit)
            | None, None ->
                PagingInfo()
#if FSHARP6
        task{
            let! result = productService.GetProducts(pagingInfo);
            return this.Ok(result) :> IActionResult;
        }
#else
        async {
            let! result = Async.AwaitTask(productService.GetProducts(pagingInfo));
            return this.Ok(result) :> IActionResult
        }
        |> Async.StartAsTask
#endif

    [<HttpGet("{id}")>]
    // public async Task<ActionResult> GetSingle(string id)
    member this.GetSingle(id: string) =
#if FSHARP6
        task{
            let! result = productService.GetSingle(id);
            return this.Ok(result) :> IActionResult;
        }
#else
        async {
            let! result = Async.AwaitTask(productService.GetSingle(id));
            return this.Ok(result) :> IActionResult;
        }
        |> Async.StartAsTask
#endif

    [<HttpPatch>]
    // public async Task<ActionResult> Patch(RatingRequest request)
    member this.Patch(request:RatingRequest): Task<IActionResult> =
#if FSHARP6
        task{
            do! productService.AddRating(request.ProductId, request.Rating);
            return this.Ok() :> IActionResult;
        }
#else
        async {
            do! Async.AwaitTask(productService.AddRating(request.ProductId, request.Rating));
            return this.Ok() :> IActionResult;
        }
        |> Async.StartAsTask
#endif
