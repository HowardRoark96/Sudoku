var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component } from '@angular/core';
import { DataService } from './data.service';
import { User } from './user';
let AppComponent = class AppComponent {
    constructor(dataService) {
        this.dataService = dataService;
        this.user = new User(); // изменяемый пользователь
        this.tableMode = true; // табличный режим
    }
    ngOnInit() {
        this.loadUsers(); // загрузка данных при старте компонента  
    }
    loadUsers() {
        this.dataService.getUsers()
            .subscribe((data) => this.users = data);
    }
    save() {
        if (this.user.id == null) {
            this.dataService.createUser(this.user)
                .subscribe((data) => this.users.push(data));
        }
        else {
            this.dataService.updateUser(this.user)
                .subscribe(data => this.loadUsers());
        }
        this.cancel();
    }
    editUser(user) {
        this.user = user;
    }
    cancel() {
        this.user = new User();
        this.tableMode = true;
    }
    delete(user) {
        this.dataService.deleteUserById(user.id)
            .subscribe(data => this.loadUsers());
    }
    add() {
        this.cancel();
        this.tableMode = false;
    }
};
AppComponent = __decorate([
    Component({
        selector: 'app',
        templateUrl: './app.component.html',
        providers: [DataService]
    })
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map