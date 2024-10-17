import { Component, OnInit } from '@angular/core';
import { Branch } from '../../models/branch';
import { BranchService } from '../../services/branch.service';
import { CommonModule } from '@angular/common';
import { BasicLayoutComponent } from '../../../../shared/components/basic-layout/basic-layout.component';

@Component({
  selector: 'app-branch-list',
  standalone: true,
  imports: [CommonModule,BasicLayoutComponent],
  templateUrl: './branch-list.component.html',
  styleUrl: './branch-list.component.scss'
})
export class BranchListComponent implements OnInit {

  branches:Branch[]=[];
  pageIndex: number = 0;
  pageSize: number = 50;
  groupedBranches: { [key: string]: Branch[] } = {};

  constructor(private branchService:BranchService,){}

  ngOnInit(): void {
    this.getBranches();
  }


  getBranches() {
    this.branchService.getBranches(this.pageIndex, this.pageSize).subscribe(response => {
      this.branches = response.items;
      this.groupBranches();
    });
  }

  groupBranches() {
    this.groupedBranches = {};
    this.branches.sort((a, b) => a.name.localeCompare(b.name, 'tr-TR')).forEach(branch => {
      const firstLetter = branch.name.charAt(0).toUpperCase();
      if (!this.groupedBranches[firstLetter]) {
        this.groupedBranches[firstLetter] = [];
      }
      this.groupedBranches[firstLetter].push(branch);
    });
  }

  groupedKeys(): string[] {
    return Object.keys(this.groupedBranches).sort((a, b) => a.localeCompare(b, 'tr'));
  }
}
