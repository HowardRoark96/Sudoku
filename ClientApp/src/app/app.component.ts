import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { User } from './user';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {

    user: User = new User();   // изменяемый пользователь
    users: User[];                // массив пользователей
    tableMode: boolean = true;          // табличный режим

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.loadUsers();    // загрузка данных при старте компонента  
    }

    loadUsers() {
        this.dataService.getUsers()
            .subscribe((data: User[]) => this.users = data);
    }

    save() {
        if (this.user.id == null) {
            this.dataService.createUser(this.user)
                .subscribe((data: User) => this.users.push(data));
        } else {
            this.dataService.updateUser(this.user)
                .subscribe(data => this.loadUsers());
        }
        this.cancel();
    }

    editUser(user: User) {
        this.user = user;
    }

    cancel() {
        this.user = new User();
        this.tableMode = true;
    }

    delete(user: User) {
        this.dataService.deleteUserById(user.id)
            .subscribe(data => this.loadUsers());
    }

    add() {
        this.cancel();
        this.tableMode = false;
    }
}