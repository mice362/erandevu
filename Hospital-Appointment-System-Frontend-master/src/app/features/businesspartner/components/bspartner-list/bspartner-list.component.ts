import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';

import { BusinessPartner } from '../../models/businesspartner'
import { BasicLayoutComponent } from '../../../../shared/components/basic-layout/basic-layout.component';
import { FormsModule } from '@angular/forms';
import { BusinessPartnerService } from '../../services/bspartner.service';


@Component({
  selector: 'app-bspartner-list',
  standalone: true,
  imports: [CommonModule,BasicLayoutComponent,FormsModule],
  templateUrl: './bspartner-list.component.html',
  styleUrl: './bspartner-list.component.scss'
})
export class BusinessPartnerListComponent implements OnInit {

  businessPartners: BusinessPartner[] = [];
  pageIndex: number = 0;
  pageSize: number = 10;
  isLoading: boolean = true;
  selectedBranch: string = '';

  constructor(private businessPartnerService: BusinessPartnerService,) {}

  ngOnInit(): void {
    this.getBusinessPartners();
  }
  getBusinessPartners() {
    this.businessPartnerService.getBusinessPartners(this.pageIndex, this.pageSize).subscribe((response) => {
      this.businessPartners = response.items;
    });
  }
}
