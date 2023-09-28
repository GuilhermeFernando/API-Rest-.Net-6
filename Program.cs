using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/user",() =>new {Name = "Guilherme", Age = 28});
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste","Guilherme");
    return new {Name = "Guilherme Machado", Age = 28};
});

app.MapPost("/produtosalvo", (Produto produto) => {
   RepositorioProduto.Add(produto);
});

app.MapGet("/getproduto/{code}",([FromRoute] string code) => {
    var produto = RepositorioProduto.GetBy(code); 
    return produto;
});

app.MapPut("/editproduto",(Produto produto) => {
    var produtoSave = RepositorioProduto.GetBy(produto.Codigo);
    produtoSave.Nome = produto.Nome;
});


app.MapDelete("/deleteproduto/{code}",([FromRoute] String code  ) => {
    var produtoSave = RepositorioProduto.GetBy(code);
    RepositorioProduto.Remove(produtoSave);
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