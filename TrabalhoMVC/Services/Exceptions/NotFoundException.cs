using System;


namespace TrabalhoMVC.Services.Exceptions {
	public class NotFoundException : ApplicationException{

		public NotFoundException(string message) : base(message){

		}

	}
}
