import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent {
  title = 'agent-portal';

  constructor(private router: Router){
    this.router.navigate(["listings"]);
  }

  ngOnInit(){

  }
}
