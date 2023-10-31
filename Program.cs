using System.Collections.Immutable;
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);
var app = builder.Build();

app.MapPost("/produto", (ProdutoDto produtoDto, ApplicationDbContext context) => {
    var categoria = context.Categorias.Where(c => c.Id == produtoDto.CategoriaId).First();
    var produto = new Produto
    {
        Codigo = produtoDto.Codigo,
        Nome = produtoDto.Nome,
        Descricao = produtoDto.Descricao,
        Categoria = categoria
    };
   context.Produtos.Add(produto);
   return Results.Created($"/produto/{produto.Id}",produto.Id);
    
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
