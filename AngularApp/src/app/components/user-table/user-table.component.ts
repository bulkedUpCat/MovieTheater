import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.css']
})
export class UserTableComponent implements OnInit {
  users: User[] = [];
  displayedColumns: string[] = ['Email','Username'];
  dataSource;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe(users =>{
      this.users = users;
      console.log(users);
      this.dataSource = new MatTableDataSource(this.users);
    })
  }

  applyFilter(event: Event){
    const input = (event.target as HTMLInputElement).value;
    this.dataSource.filter = input.trim().toLowerCase();
  }
}
