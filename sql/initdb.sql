INSERT INTO inventory.customers (id, first_name, last_name, email)
VALUES (1005, 'Alice', 'Nguyen', 'alice.nguyen@example.com')

update inventory.customers 
set first_name='Vinh', last_name='Nguyen', email='vinhnt1@email.vn'
where id = 1005
