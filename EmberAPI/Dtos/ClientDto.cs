﻿namespace EmberAPI.Dtos;

public class ClientDto
{
    public int ClientID { get; set; }
    public string ClientName { get; set; }
    public bool Status { get; set; }
    public DateOnly CreationDate { get; set; }
}