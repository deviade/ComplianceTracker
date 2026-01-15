# **README.md - Compliance Tracker Project**

## **Project Status**

✅ **COMPLETED:**
1. **Backend API** - Full REST API with contractors and documents endpoints
2. **Data Model** - Entity Framework Core with Code First migrations
3. **Blazor Frontend** - Working contractors list page with API integration

## **Tasks for Part 3 & 4 (With More Time)**

### **Part 3 – SQL Tasks (To Be Implemented)**

If I had more time, I would create `SQL_Tasks.sql` with:

#### **Schema Script**
- CREATE TABLE scripts for all entities (Contractors, ContractorDocuments, DocumentTypes)
- Foreign key constraints and indexes
- Sample seed data for testing

#### **Required Queries:**
1. **Contractors with expired documents + count:**
   ```sql
   -- Query all contractors with at least one expired document
   -- Count of expired documents per contractor
   ```

2. **Top 5 contractors with most expired/expiring documents:**
   ```sql
   -- Rank contractors by total expired + expiring-soon documents
   -- Show document counts by status
   ```

3. **Indexing suggestions:**
   - Index on `ContractorDocuments.ExpiryDate` for expiration queries
   - Composite index on `(ContractorId, Status)` for contractor-specific queries
   - Index on `ContractorDocuments.Status` for status-based filters

### **Part 4 – Architecture & Azure (With More Time)**

If I had more time, I would create `Architecture_Plan.md` covering:

#### **Azure Deployment:**
- **API**: Azure App Service or Azure Container Apps
- **Blazor App**: Azure Static Web Apps or App Service
- **Database**: Azure SQL Database with geo-replication
- **Secrets**: Azure Key Vault for connection strings
- **Logging**: Application Insights with Log Analytics

#### **Scalability & Performance:**
- **Caching**: Redis Cache for frequently accessed contractors
- **DB Optimization**: Read replicas for reporting queries
- **Scaling**: Auto-scaling App Service plans based on CPU/memory

#### **Security:**
- **Authentication**: Azure AD B2C for contractor portal
- **Encryption**: Always Encrypted for sensitive data
- **Roles**: RBAC with contractor/admin roles
- **Azure Features**: Managed Identities for service-to-service auth

#### **Resilience & Operations:**
- **Failover**: Multi-region deployment with Traffic Manager
- **Monitoring**: Azure Monitor with custom dashboards
- **Alerts**: Proactive alerts for API failures and expiring documents
- **Backups**: Automated SQL backups with point-in-time restore

## **Next Steps**

With additional time, I would:
1. Complete SQL queries and optimization scripts
2. Implement Azure DevOps pipeline for CI/CD
3. Add authentication and authorization
4. Implement remaining Blazor pages (details, expiring documents)
5. Add comprehensive unit and integration tests
6. Set up monitoring and alerting
7. Deploy to Azure staging environment

---
*Note: The current implementation focuses on core functionality with a working API and frontend. The project is structured to easily add the remaining features.*
