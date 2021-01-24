import { Message } from '../_models/message';
import { Component, OnInit } from '@angular/core';
import { Pagination } from '../_models/pagination';
import { MembersService } from '../_services/members.service';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages : Message[] =[];
  pagination:Pagination;
  container ='Outbox';
  pagNumber=1;
  pagSize=5;

  constructor(private messageService:MessageService) { }

  ngOnInit(): void {
    this.loadMessages();
  }
  loadMessages()
  {
    this.messageService.getMessages(this.pagNumber,this.pagSize,this.container).subscribe(response =>{
      this.messages = response.result;
      this.pagination = response.pagination;
    })
  }
  deleteMessage(id:number)
  {
    this.messageService.deleteMessage(id).subscribe(()=>{
      this.messages.splice(this.messages.findIndex(m => m.id ===id),1);
    })
  }
  pageChanged(event :any)
  {
    this.pagNumber =event.page;
    this.loadMessages();
  }

}
