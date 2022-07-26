namespace ContosoCrafts.ProductsApi.Models

open System;
open System.Text.Json.Serialization;
open MongoDB.Bson;
open MongoDB.Bson.Serialization.Attributes;

type Product() =
    [<BsonId>]
    [<BsonElement("_id")>]
    [<JsonIgnore>]
    [<BsonRepresentation(BsonType.ObjectId)>]
    member val RecId = null with get,set

    [<BsonRepresentation(BsonType.String)>]
    [<BsonElement("Id")>]
    [<JsonPropertyName("Id")>]
    member val ProductId : string = null with get,set
    member val Maker : string = null with get,set
    member val Image : string = null with get,set
    member val Url : string = null with get,set
    member val Title : string = null with get,set
    member val Description : string = null with get,set
    member val Ratings : int[] = null with get,set
