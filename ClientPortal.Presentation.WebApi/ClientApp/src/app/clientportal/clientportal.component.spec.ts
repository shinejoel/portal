import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientportalComponent } from './clientportal.component';

describe('ClientportalComponent', () => {
  let component: ClientportalComponent;
  let fixture: ComponentFixture<ClientportalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientportalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientportalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
