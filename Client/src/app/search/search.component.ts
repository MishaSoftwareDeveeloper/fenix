import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BackendService } from "../services/backend.service";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  repos: Array<any> = [];
  constructor(private router: Router, private api: BackendService) {
    let user = JSON.parse(sessionStorage.getItem('user'));
    if (user && user.id > 0) {
      this.fillData();
    }
    else {
      this.router.navigate(['/login'])
    }
  }

  ngOnInit(): void {

  }
  fillData() {
    this.api.getAllReps().subscribe((res: any) => {
      if (res)
        this.repos = res.items;
    })
  }
  onSearch(repo) {
    if (repo.length > 4) {
      this.api.findReps(repo).subscribe((res: any) => {
        if (res)
          this.repos = res.items;
      })
    }
  }
  onAddRepo(repo) {
    let dataTosend = {
      id: repo.id,
      name: repo.name,
      image: repo.owner.avatar_url
    }
    debugger
    this.api.insert('reps', dataTosend).subscribe((res) => { });
  }
}
