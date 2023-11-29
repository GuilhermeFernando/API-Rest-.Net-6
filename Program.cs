using System.Collections.Immutable;
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
    if(produtoDto.Tags != null)
    {
        produto.Tags = new List<Tag>();
        foreach(var item in produtoDto.Tags)
        {
            produto.Tags.Add(new Tag {Nome = item});
        }
    }
   context.Produtos.Add(produto);
   context.SaveChanges();
   return Results.Created($"/produto/{produto.Id}",produto.Id);
    
});

app.MapGet("/produto/{id}",([FromRoute] int id, ApplicationDbContext context) => {
    var produto = context.Produtos
    .Include(j => j.Categoria)
    .Include(j => j.Tags)
    .Where(j => j.Id == id).First(); 
   if(produto != null)
   {
    return Results.Ok(produto);
   }
   return Results.NotFound();
});

app.MapPut("/produto/{id}",([FromRoute] int id, ProdutoDto produtoDto,ApplicationDbContext context) => {
     var produto = context.Produtos    
    .Include(j => j.Tags)
    .Where(j => j.Id == id).First(); 
    var categoria = context.Categorias.Where(c => c.Id == produtoDto.CategoriaId).First();

    produto.Codigo = produtoDto.Codigo;
    produto.Nome = produtoDto.Nome;
    produto.Descricao = produtoDto.Descricao;  
    produto.Categoria = categoria;
    produto.Tags = new List<Tag>();
    if(produtoDto.Tags != null)
    {
        produto.Tags = new List<Tag>();
        foreach(var item in produtoDto.Tags)
        {
            produto.Tags.Add(new Tag {Nome = item});
        }
    }
    context.SaveChanges();
    return Results.Ok(produto);
  
});

app.MapDelete("/produto/{code}",([FromRoute] String code  ) => {
    var produtoSave = RepositorioProduto.GetBy(code);
    RepositorioProduto.Remove(produtoSave);
    return Results.Ok();
});

app.Run();
