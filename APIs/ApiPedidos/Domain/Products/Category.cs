﻿using System.Diagnostics.Contracts;
using Flunt.Validations;

namespace ApiPedidos.Domain.Products;

public class Category : Entity
{
    public bool Active { get; set; }

    public Category(string name, string createdBy, string editedBy)
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name", "Name é obrigatório !");
        AddNotifications(contract);

        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;
    }
}
