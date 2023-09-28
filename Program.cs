using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/produto", (Produto produto) => {
   RepositorioProduto.Add(produto);
   return Results.Created($"/produto/{produto.Codigo}",produto.Codigo);
    
});

app.MapGet("/produto/{code}",([FromRoute] string code) => {
    var produto = RepositorioProduto.GetBy(code); 
   if(produto != null)
   {
    return Results.Ok(produto);
   }
   return Results.NotFound();
});

app.MapPut("/produto",(Produto produto) => {
    var produtoSave = RepositorioProduto.GetBy(produto.Codigo);
    produtoSave.Nome = produto.Nome;
    return Results.Ok();
});

app.MapDelete("/produto/{code}",([FromRoute] String code  ) => {
    var produtoSave = RepositorioProduto.GetBy(code);
    RepositorioProduto.Remove(produtoSave);
    return Results.Ok();
});

app.Run();


public static class RepositorioProduto
{

    public static List<Produto> Produtos{get;set;}

    public static void Add(Produto produtos)
    {
        if(Produtos == null)
        {
            Produtos = new List<Produto>();
            Produtos.Add(produtos);
        }
    }

    public static Produto GetBy(string codigo)
    {
        return Produtos.FirstOrDefault(p => p.Codigo == codigo);
    }

    public static void Remove(Produto produto)
    {
        Produtos.Remove(produto);
    }

}
public class Produto
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
    
}