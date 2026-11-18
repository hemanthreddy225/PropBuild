export type Status = 'active' | 'overdue' | 'expiring' | 'vacating';
export type Tenant = { id: string; name: string; property: string; rent: number; deposit: number; status: Status; email: string; phone: string; start: string; end: string; notes: string };
// UI-only fixture data. Replace with API queries during integration.
export const mockTenants: Tenant[] = [
  { id: '1', name: 'Arjun Mehta', property: 'Koramangala 4B Flat', rent: 28000, deposit: 56000, status: 'active', email: 'arjun.mehta@gmail.com', phone: '+91 98450 12345', start: '2024-04-01', end: '2026-03-31', notes: 'Long-term tenant. Very punctual with payments.' },
  { id: '2', name: 'Priya Nair', property: 'Whitefield Villa', rent: 45000, deposit: 90000, status: 'active', email: 'priya.nair@hotmail.com', phone: '+91 97890 54321', start: '2023-06-15', end: '2026-06-14', notes: 'Family of 4. Excellent tenant.' },
  { id: '3', name: 'Vikram Joshi', property: 'Indiranagar 2BHK', rent: 22000, deposit: 44000, status: 'overdue', email: 'vikram.j@yahoo.com', phone: '+91 99001 67890', start: '2025-03-01', end: '2026-04-30', notes: 'March rent is pending.' },
  { id: '4', name: 'Deepa Krishnan', property: 'Indiranagar 2BHK', rent: 24000, deposit: 48000, status: 'expiring', email: 'deepa.k@gmail.com', phone: '+91 96500 11223', start: '2025-07-01', end: '2026-06-30', notes: 'Wants to renew for another year.' }
];
